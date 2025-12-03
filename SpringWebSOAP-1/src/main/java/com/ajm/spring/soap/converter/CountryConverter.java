package com.ajm.spring.soap.converter;

import java.util.ArrayList;
import java.util.List;

import org.springframework.stereotype.Component;
import com.ajm.soap.gen.Country;
import com.ajm.spring.soap.model.CountryModel;

@Component
public class CountryConverter {
	public CountryModel converCountryToCountryModel(Country country) {
		CountryModel countryModel = new CountryModel();
		countryModel.setName(country.getName());
		countryModel.setCapital(country.getCapital());
		countryModel.setPopulation(country.getPopulation());
		countryModel.setCurrency(country.getCurrency());
		return countryModel;
	}
	
	public Country converCountryModelToCountry(CountryModel countryModel) {
		Country country = new Country();
		
		country.setName(countryModel.getName());
		country.setCapital(countryModel.getCapital());
		country.setPopulation(countryModel.getPopulation());
		country.setCurrency(countryModel.getCurrency());
		
		return country;
	}
	
	public CountryModel updateCountryToCountryModel(Country country, CountryModel countryModel) {
		if(!country.getCapital().isBlank()) {
			countryModel.setCapital(country.getCapital());
		}
		if(country.getPopulation()!=0) {
			countryModel.setPopulation(country.getPopulation());
		}
		if(!country.getCurrency().isBlank()) {
			countryModel.setCurrency(country.getCurrency());
		}
		return countryModel;
	}

	public List<Country> converCountriesModelToCountries(List<CountryModel> countriesModel) {
		List<Country> countries = new ArrayList<Country>();
		for(CountryModel countryModel: countriesModel) {
			countries.add(converCountryModelToCountry(countryModel));
			
		}
		return countries;
	}
}
