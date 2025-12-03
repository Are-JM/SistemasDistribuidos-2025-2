package com.ajm.spring.soap.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.ajm.spring.soap.model.CountryModel;

@Repository
public interface CountryRepository extends JpaRepository<CountryModel, Integer> {
	CountryModel findByName(String name);
}
