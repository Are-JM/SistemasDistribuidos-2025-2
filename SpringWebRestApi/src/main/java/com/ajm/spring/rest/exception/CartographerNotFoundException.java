package com.ajm.spring.rest.exception;

public class CartographerNotFoundException extends RuntimeException {
    public CartographerNotFoundException(String message) {
        super(message);
    }

    public CartographerNotFoundException(String message, Throwable cause) {
        super(message, cause);
    }
}
