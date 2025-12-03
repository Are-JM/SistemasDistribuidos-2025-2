package com.ajm.spring.rest.dtos;

import java.io.Serializable;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import lombok.AllArgsConstructor;
import lombok.Data;
@Data
@AllArgsConstructor
@JsonIgnoreProperties(ignoreUnknown = true)
public class CountryCreateDto implements Serializable {

	private static final long serialVersionUID = 1L;

	private String name;
	private String capital;
	private int population;
	private String currency;
	
	
}
