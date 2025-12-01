package com.ajm.spring.rest.mappers;
import java.time.Instant;
import java.util.List;
import java.util.stream.Collectors;

import com.ajm.spring.rest.dtos.CartographerCreateDto;
import com.ajm.spring.rest.dtos.CartographerResponseDto;
import com.ajm.spring.rest.dtos.CreateCartographerResponseDto;

import cartographerspb.Cartographer.CartographerResponse;
import cartographerspb.Cartographer.CreateCartographerRequest;
import cartographerspb.Cartographer.CreateCartographerResponse;

public class CartographerMapper {
    public static CreateCartographerRequest toProto(CartographerCreateDto dto) {
        return CreateCartographerRequest.newBuilder()
                .setName(dto.getName())
                .setCompany(dto.getCompany())
                .setAge(dto.getAge())
                .build();
    }

    public static CartographerResponseDto toDto(CartographerResponse proto) {
        Instant createdAt = Instant.ofEpochSecond(
                proto.getCreatedAt().getSeconds(),
                proto.getCreatedAt().getNanos()
        );

        return new CartographerResponseDto(
                proto.getId(),
                proto.getName(),
                proto.getCompany(),
                proto.getAge(),
                createdAt
        );
    }

    public static List<CartographerResponseDto> toDtoList(
            List<CartographerResponse> protos) {
        return protos.stream()
                .map(CartographerMapper::toDto)
                .collect(Collectors.toList());
    }

    public static CreateCartographerResponseDto toDto(
            CreateCartographerResponse proto) {
        return new CreateCartographerResponseDto(
                proto.getSuccessCount(),
                toDtoList(proto.getCartographersList())
        );
    }
}
