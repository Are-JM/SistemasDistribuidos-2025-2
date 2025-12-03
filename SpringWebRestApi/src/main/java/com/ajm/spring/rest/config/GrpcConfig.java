package com.ajm.spring.rest.config;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import cartographerspb.CartographerServiceGrpc;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;

@Configuration
public class GrpcConfig {
    private static final Logger logger = LoggerFactory.getLogger(GrpcConfig.class);
    
    @Bean
    public ManagedChannel grpcManagedChannel(
        @Value("${grpc.host}")  String grpcHost,
        @Value("${grpc.port}") int grpcPort
    ) {
        logger.info("Creating gRPC ManagedChannel to {}:{}", grpcHost, grpcPort);
        ManagedChannel channel = ManagedChannelBuilder.forAddress(grpcHost, grpcPort)
                .usePlaintext()
                .build();
        logger.info("gRPC ManagedChannel created successfully");
        return channel;
    }

    @Bean
    public CartographerServiceGrpc.CartographerServiceBlockingStub blockingStub(ManagedChannel channel) {
        logger.info("Creating gRPC BlockingStub");
        return CartographerServiceGrpc.newBlockingStub(channel);
    }

    @Bean
    public CartographerServiceGrpc.CartographerServiceStub asyncStub(ManagedChannel channel) {
        logger.info("Creating gRPC AsyncStub");
        return CartographerServiceGrpc.newStub(channel);
    }


}
