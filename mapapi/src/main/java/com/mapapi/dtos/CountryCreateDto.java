package com.mapapi.dtos;
import java.io.Serializable;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import lombok.Data;

@Data
@JsonIgnoreProperties(ignoreUnknown = true)
public class CountryCreateDto implements Serializable{


    private String name;

    public String getName() {
        return name;
    }
    private String capital;
    public String getCapital() {
        return capital;
    }
    private long population;
    public long getPopulation() {
        return population;
    }
    private String currency;
    public CountryCreateDto(String name, String capital, long population, String currency) {
        this.name = name;
        this.capital = capital;
        this.population = population;
        this.currency = currency;
    }
    public CountryCreateDto() {
    }
    public String getCurrency() {
        return currency;
    }
}
