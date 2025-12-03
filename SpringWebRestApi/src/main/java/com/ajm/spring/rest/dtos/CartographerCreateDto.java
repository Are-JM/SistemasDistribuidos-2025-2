package com.ajm.spring.rest.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class CartographerCreateDto {
    private String name;
    private String company;
    private int age;
}
