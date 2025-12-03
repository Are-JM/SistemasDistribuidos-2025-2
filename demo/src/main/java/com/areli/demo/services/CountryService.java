package com.areli.demo.services;

import com.areli.demo.domain.Country;

public interface CountryService {

    Country getCountryById(long countryId);

    Country addCountry(Country country);
}