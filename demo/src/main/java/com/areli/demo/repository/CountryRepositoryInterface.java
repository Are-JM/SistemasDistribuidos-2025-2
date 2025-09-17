package com.areli.demo.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.areli.demo.domain.Country;

@Repository
public interface CountryRepositoryInterface extends JpaRepository<Country, Long>{
    Country findCountryById(Long Id);
}
