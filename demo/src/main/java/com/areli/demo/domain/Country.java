package com.areli.demo.domain;


import java.io.Serializable;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import jakarta.validation.constraints.Positive;
import jakarta.persistence.*;
import lombok.*;


@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table
public class Country implements Serializable{
    private static final long serialVersionUID = 1L;
    @Id 
    @GeneratedValue (strategy = GenerationType.AUTO)
    //@Column(name = "country_id")
    @Column
    private Long Id;

    public String getId() {
		return name;
	}

	public void setId(String name) {
		this.name = name;
	}
    //@Column(name = "name")
    @Column
    @NotNull
    @Size(min=3,max=50)
    private String name;

    public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
    //@Column(name = "population")
    @Column
    @Positive
    private Long population;

    public Long getPopulation() {
		return population;
	}

	public void setPopulation(Long population) {
		this.population = population;
	}

    //@Column(name = "capital")
    @Column
    @NotNull
    @Size(min=3,max=50)
    private String capital;

    public String getCapital() {
		return capital;
	}

	public void setCapital(String capital) {
		this.capital = capital;
	}

    @Column
    @NotNull
    @Size(min=2,max=50)
    private String currency;
 
    public String getCurrency() {
		return currency;
	}

	public void setCurrency(String currency) {
		this.currency = currency;
	}
}
