package com.ajm.spring.soap.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;
import jakarta.validation.constraints.Size;
import lombok.Data;

@Entity
@Data
public class CountryModel {
	@Id
	@GeneratedValue(strategy= GenerationType.AUTO)
	private Integer id;
	@NotNull
	@Size(min=3,max = 50)
	private String name;
	@Positive
	private Integer population;
	@NotNull
	@Size(min=3,max = 50)
	private String capital;
	@NotNull
	@Size(min=2,max = 50)
	private String currency;

}
