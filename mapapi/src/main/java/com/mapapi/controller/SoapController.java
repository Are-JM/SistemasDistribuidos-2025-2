package com.mapapi.controller;

import org.springframework.web.bind.annotation.RestController;

import com.mapapi.wsdl.GetCountryResponse;
import com.mapapi.wsdl.CountryInfo;
import com.mapapi.wsdl.CreateCountryRequest;
import com.mapapi.wsdl.CreateCountryResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cache.Cache;
import org.springframework.cache.CacheManager;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

import com.mapapi.client.SoapClient;
import com.mapapi.dtos.CountryCreateDto;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;




@RestController
public class SoapController {
    @Autowired
    private SoapClient soapClient;

    @Autowired
    private CacheManager cacheManager;




    @GetMapping("/country/{id}")
    public ResponseEntity<?> getCountry(@PathVariable long id) {

        if (id <= 0) {
        return ResponseEntity.badRequest().body("ID inválido. Debe ser mayor a 0");
        }

        try {
            GetCountryResponse response = soapClient.getCountryResponse(id);

            if (response == null || response.getCountryInfo() == null) {
                return ResponseEntity.status(404).body("País no encontrado con ID: " + id);
            }

            cacheManager.getCache("countries").put(id, response.getCountryInfo());
            return ResponseEntity.ok(response.getCountryInfo());

        } catch (Exception ex) {
            return ResponseEntity.status(404).body("País no encontrado con ID: " + id);
        }
    }

    @PostMapping("/country")
    public ResponseEntity<?> createCountry(@RequestBody CountryCreateDto dto) {

        if (dto.getName() == null || dto.getName().trim().isEmpty()) {
            return ResponseEntity.badRequest().body("El nombre del país es obligatorio");
        }

        if (dto.getCapital() == null || dto.getCapital().trim().isEmpty()) {
            return ResponseEntity.badRequest().body("La capital es obligatoria");
        }

        if (dto.getCurrency() == null || dto.getCurrency().trim().isEmpty()) {
            return ResponseEntity.badRequest().body("La moneda es obligatoria");
        }

        if (dto.getPopulation() <= 0) {
            return ResponseEntity.badRequest().body("La población debe ser mayor a 0");
        }

        CreateCountryRequest request = new CreateCountryRequest();

        CountryInfo countryInfo = new CountryInfo();
        countryInfo.setName(dto.getName());
        countryInfo.setCapital(dto.getCapital());
        countryInfo.setPopulation(dto.getPopulation());
        countryInfo.setCurrency(dto.getCurrency());

        request.setCountryInfo(countryInfo);

        CreateCountryResponse response = soapClient.createCountryResponse(countryInfo);
        return ResponseEntity.ok(response.getCountryInfo());
    }
    
    
}