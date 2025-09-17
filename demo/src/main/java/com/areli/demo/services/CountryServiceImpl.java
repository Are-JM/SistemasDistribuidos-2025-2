package com.areli.demo.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.areli.demo.domain.Country;
import com.areli.demo.repository.CountryRepositoryInterface;

@Service
public class CountryServiceImpl implements CountryService {
    @Autowired
    CountryRepositoryInterface countryRepository;

    @Override
    public Country getCountryById(long countryId){
        Country obj = countryRepository.findCountryById(countryId);
        return obj;
    }

    @Override
    public Country addCountry(Country country){
        return countryRepository.save(country);
    }

     
}
