package com.ajm.spring.rest.client;

import cartographerspb.CartographerServiceGrpc;
import cartographerspb.Cartographer.CartographerIdRequest;
import cartographerspb.Cartographer.CartographerNameRequest;
import cartographerspb.Cartographer.CartographerResponse;
import cartographerspb.Cartographer.CreateCartographerRequest;
import cartographerspb.Cartographer.CreateCartographerResponse;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import com.ajm.spring.rest.dtos.CartographerCreateDto;
import com.ajm.spring.rest.dtos.CartographerResponseDto;
import com.ajm.spring.rest.dtos.CreateCartographerResponseDto;
import com.ajm.spring.rest.exception.CartographerNotFoundException;
import com.ajm.spring.rest.mappers.CartographerMapper;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

@Service
public class GrpcClient {
    private static final Logger logger = LoggerFactory.getLogger(GrpcClient.class);
    private final CartographerServiceGrpc.CartographerServiceBlockingStub blockingStub;
    private final CartographerServiceGrpc.CartographerServiceStub asyncStub;

    public GrpcClient (CartographerServiceGrpc.CartographerServiceBlockingStub blockingStub, CartographerServiceGrpc.CartographerServiceStub asyncStub) {
        this.blockingStub = blockingStub;
        this.asyncStub = asyncStub;
        logger.info("GrpcClient initialized with blocking and async stubs");
    }

    public CartographerResponseDto getById(String id) {
        logger.info("Attempting to get cartographer by id: {}", id);
        
        if (id.length() != 24) {
            String errorMsg = String.format("Invalid ID format. Expected 24 characters");
            logger.error(errorMsg);
            throw new IllegalArgumentException(errorMsg);
        }
        
        try {
            logger.debug("Request with valid ID: {}", id);
            CartographerIdRequest request = CartographerIdRequest.newBuilder().setId(id).build();
            CartographerResponse response = blockingStub.getById(request);
            
            if (response == null || response.getId() == null || response.getId().isEmpty()) {
                String errorMsg = String.format("Cartographer doesn't exist with id: %s", id);
                logger.error(errorMsg);
                throw new CartographerNotFoundException(errorMsg);
            }
            
            logger.info("Successfully retrieved cartographer with ID: {}", response.getId());
            return CartographerMapper.toDto(response);
        } catch (CartographerNotFoundException e) {
            // Re-lanzar la excepción de no encontrado
            throw e;
        } catch (StatusRuntimeException e) {
            // Capturar errores del servidor gRPC
            Status status = e.getStatus();
            String errorMsg = String.format("gRPC error: %s - %s", status.getCode(), status.getDescription());
            logger.error("gRPC status error for ID {}: {}", id, errorMsg, e);
            
            // Si es NOT_FOUND (5), convertir a CartographerNotFoundException
            if (status.getCode() == Status.Code.NOT_FOUND) {
                throw new CartographerNotFoundException(String.format("Cartographer doesn't exist with id: %s", id), e);
            }
            
            // Para otros errores gRPC
            throw new RuntimeException("Failed to get cartographer by id: " + id + ". Error: " + errorMsg, e);
        } catch (IllegalArgumentException e) {
            logger.error("Validation error for ID {}: {}", id, e.getMessage());
            throw e;
        } catch (Exception e) {
            logger.error("Unexpected error getting cartographer by id: {}", id, e);
            throw new RuntimeException("Failed to get cartographer by id: " + id + ". Error: " + e.getMessage(), e);
        }
    }

    public List<CartographerResponseDto> getByName(String name) {
        logger.info("Attempting to get cartographers by name: {}", name);
        
        if (name == null || name.isEmpty()) {
            String errorMsg = "Name parameter cannot be null or empty";
            logger.error(errorMsg);
            throw new IllegalArgumentException(errorMsg);
        }
        
        try {
            CartographerNameRequest request = CartographerNameRequest.newBuilder().setName(name).build();
            List<CartographerResponse> list = new ArrayList<>();
            blockingStub.getCartographersByName(request).forEachRemaining(list::add);
            logger.info("Retrieved {} cartographers with name: {}", list.size(), name);
            return CartographerMapper.toDtoList(list);
        } catch (IllegalArgumentException e) {
            logger.error("Validation error for name {}: {}", name, e.getMessage());
            throw e;
        } catch (Exception e) {
            logger.error("gRPC error getting cartographers by name: {}", name, e);
            throw new RuntimeException("Failed to get cartographers by name: " + name + ". Error: " + e.getMessage(), e);
        }
    }

    public CreateCartographerResponseDto createCartographers(List<CartographerCreateDto> dtos) {
        CountDownLatch latch = new CountDownLatch(1);
        final CreateCartographerResponse[] responseHolder = new CreateCartographerResponse[1];
        final Throwable[] exceptionHolder = new Throwable[1];

        StreamObserver<CreateCartographerResponse> responseObserver =
                new StreamObserver<>() {

            @Override
            public void onNext(CreateCartographerResponse response) {
                responseHolder[0] = response;
                logger.info("Received gRPC response with {} cartographers", response.getCartographersList().size());
            }

            @Override
            public void onError(Throwable t) {
                exceptionHolder[0] = t;
                logger.error("gRPC streaming error", t);
                latch.countDown();
            }

            @Override
            public void onCompleted() {
                logger.info("gRPC streaming completed successfully");
                latch.countDown();
            }
        };

        StreamObserver<CreateCartographerRequest> requestObserver = asyncStub.createCartographers(responseObserver);

        for (CartographerCreateDto dto : dtos) {
            requestObserver.onNext(CartographerMapper.toProto(dto));
        }

        requestObserver.onCompleted();

        try {
            if (!latch.await(10, TimeUnit.SECONDS)) {
                throw new RuntimeException("Timeout waiting for gRPC response");
            }
            
            if (exceptionHolder[0] != null) {
                throw new RuntimeException("gRPC streaming failed", exceptionHolder[0]);
            }
            
            if (responseHolder[0] == null) {
                throw new RuntimeException("No response received from gRPC server");
            }
            
            return CartographerMapper.toDto(responseHolder[0]);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            logger.error("Interrupted while waiting for gRPC response", e);
            throw new RuntimeException("Interrupted while waiting gRPC response", e);
        }
    }
}
