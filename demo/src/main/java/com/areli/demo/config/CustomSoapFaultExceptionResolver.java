package com.areli.demo.config;
import org.springframework.ws.soap.server.endpoint.SoapFaultMappingExceptionResolver;
import org.springframework.ws.soap.SoapFault;

import jakarta.validation.ConstraintViolationException;

public class CustomSoapFaultExceptionResolver extends SoapFaultMappingExceptionResolver {

    @Override
    protected void customizeFault(Object endpoint, Exception ex, SoapFault fault) {
        if (ex instanceof ConstraintViolationException) {
            fault.addFaultDetail().addNamespaceDeclaration("ValidationError", ex.getMessage());
        }
    }
}
