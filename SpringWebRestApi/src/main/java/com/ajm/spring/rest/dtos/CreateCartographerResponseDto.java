package com.ajm.spring.rest.dtos;

import java.util.List;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class CreateCartographerResponseDto {
    private int successCount;
    private List<CartographerResponseDto> cartographers;
}
