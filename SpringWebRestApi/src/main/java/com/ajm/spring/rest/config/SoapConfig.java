package com.ajm.spring.rest.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.oxm.jaxb.Jaxb2Marshaller;

import com.ajm.spring.rest.client.SoapClient;

@Configuration
public class SoapConfig {
	
	@Bean
    public Jaxb2Marshaller marshaller(){
        Jaxb2Marshaller marshaller = new Jaxb2Marshaller();
        marshaller.setContextPath("com.ajm.spring.rest.wsdl");
        return marshaller;
    }

    @Bean
    public SoapClient getSoapClient(Jaxb2Marshaller marshaller){
        SoapClient soapClient = new SoapClient();
        soapClient.setDefaultUri("http://spring-app:8082/ws");
        soapClient.setMarshaller(marshaller);
        soapClient.setUnmarshaller(marshaller);

        return soapClient;
    }
}
