package com.ajm.spring.rest.dtos;

import java.time.Instant;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class CartographerResponseDto {
    private String id;
    private String name;
    private String company;
    private int age;
    private Instant createdAt;
}
