package com.ajm.spring.rest.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cache.CacheManager;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PatchMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.ajm.spring.rest.client.SoapClient;
import com.ajm.spring.rest.dtos.CountryCreateDto;
import com.ajm.spring.rest.wsdl.Country;
import com.ajm.spring.rest.wsdl.CreateCountryRequest;
import com.ajm.spring.rest.wsdl.CreateCountryResponse;
import com.ajm.spring.rest.wsdl.DeleteCountryResponse;
import com.ajm.spring.rest.wsdl.GetAllCountriesResponse;
import com.ajm.spring.rest.wsdl.GetCountryResponse;
import com.ajm.spring.rest.wsdl.UpdateCountryResponse;

@RestController
public class SoapController {
	
	@Autowired
    private SoapClient soapClient;

    @Autowired
    private CacheManager cacheManager;




    @GetMapping("/country/{name}")
    public ResponseEntity<?> getCountry(@PathVariable String name) {
        if (name.length() <= 3) {
        	return ResponseEntity.badRequest().body("Nombre inválido. Debe ser mayor a 3");
        }

        try {
            GetCountryResponse response = soapClient.getCountryResponse(name);

            if (response == null || response.getCountry() == null || response.getCountry().getName().equals("")) {
                return ResponseEntity.status(404).body("País no encontrado con Nombre: " + name);
            }

            cacheManager.getCache("country").put(name, response.getCountry());
            return ResponseEntity.ok(response.getCountry());

        } catch (Exception ex) {
            return ResponseEntity.status(404).body("País no encontrado con Nombre: " + name);
        }
    }

    @GetMapping("/country/")
    public ResponseEntity<?> getCountries() {
        try {
            GetAllCountriesResponse response = soapClient.getAllCountryResponse();

            if (response == null || response.getCountry() == null) {
                return ResponseEntity.status(404).body("No hay conenido en la tabla Paises " );
            }

            return ResponseEntity.ok(response.getCountry());

        } catch (Exception ex) {
            return ResponseEntity.status(404).body("No hay conenido en la tabla Paises " );
        }
    }
    
    @PostMapping("/country/")
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
        try {
        CreateCountryRequest request = new CreateCountryRequest();

        Country countryInfo = new Country();
        countryInfo.setName(dto.getName());
        countryInfo.setCapital(dto.getCapital());
        countryInfo.setPopulation(dto.getPopulation());
        countryInfo.setCurrency(dto.getCurrency());

        request.setCountry(countryInfo);

        CreateCountryResponse response = soapClient.createCountryResponse(countryInfo);
        return ResponseEntity.status(201).body(response.getStatus());
        } catch (Exception ex) {
            return ResponseEntity.status(404).body("País no se pudo crear" );
        }
    }
    
    @PatchMapping("/country/{name}")
    public ResponseEntity<?> updateCountry(@RequestBody CountryCreateDto dto) {

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
        try {
        Country countryInfo = new Country();
        countryInfo.setName(dto.getName());
        countryInfo.setCapital(dto.getCapital());
        countryInfo.setPopulation(dto.getPopulation());
        countryInfo.setCurrency(dto.getCurrency());


        UpdateCountryResponse response = soapClient.updateCountryResponse(countryInfo);
        return ResponseEntity.ok(response.getStatus());
    } catch (Exception ex) {
        return ResponseEntity.status(404).body("País no encontrado con Nombre: " );
    }
    }
    
    @DeleteMapping("/country/{name}")
    public ResponseEntity<?> updateCountry(@PathVariable String name) {

        if (name.length() <= 3) {
            return ResponseEntity.badRequest().body("Nombre inválido. Debe ser mayor a 3");
        }
        try {
        Country countryInfo = new Country();
        countryInfo.setName(name);
        
        DeleteCountryResponse response = soapClient.deleteCountryResponse(countryInfo);
        return ResponseEntity.status(204).body(response.getStatus());
        } catch (Exception ex) {
            return ResponseEntity.status(404).body("País no encontrado con Nombre: " + name);
        }
    }
}
