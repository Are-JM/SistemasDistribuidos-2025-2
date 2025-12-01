package com.ajm.spring.rest.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;
import com.ajm.spring.rest.client.GrpcClient;
import com.ajm.spring.rest.dtos.CartographerCreateDto;
import com.ajm.spring.rest.dtos.CartographerResponseDto;
import com.ajm.spring.rest.dtos.CreateCartographerResponseDto;

import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.PostMapping;



@RestController
@RequestMapping("/cartographers")
public class GrpcController {

    private final GrpcClient grpcClient;

    public GrpcController(GrpcClient grpcClient) {
        this.grpcClient = grpcClient;
    }

    @GetMapping("/id/{id}")
    public CartographerResponseDto getCartographerById(@PathVariable String id) {
        return grpcClient.getById(id);
    }

    @GetMapping("/by-name")
    public List<CartographerResponseDto> getByName(@RequestParam String name) {
        return grpcClient.getByName(name);
    }

    @PostMapping
    public CreateCartographerResponseDto createCartographers(
            @RequestBody List<CartographerCreateDto> requests) {
        return grpcClient.createCartographers(requests);
    }
    
}
