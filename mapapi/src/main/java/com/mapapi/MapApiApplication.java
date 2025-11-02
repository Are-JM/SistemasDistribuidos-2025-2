package com.mapapi;

import java.util.logging.Logger;

import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cache.annotation.EnableCaching;
import org.springframework.context.annotation.Bean;

import com.mapapi.client.SoapClient;
import com.mapapi.wsdl.CountryInfo;
import com.mapapi.wsdl.CreateCountryResponse;
import com.mapapi.wsdl.GetCountryResponse;



@SpringBootApplication
@EnableCaching
public class MapApiApplication {

	private static final Logger LOGGER = Logger.getLogger(MapApiApplication.class.getName());
	public static void main(String[] args) {
		SpringApplication.run(MapApiApplication.class, args);
	}

	/**@Bean
	CommandLineRunner init(SoapClient client) {
		return args -> {
			// Example usage:
			LOGGER.info("Country Name: CommandLineRunner");
			GetCountryResponse country = client.getCountryResponse(1l);
			System.out.println("Country Name: " + country.getCountryInfo().getName());

			LOGGER.info("Country Name: " + country.getCountryInfo().getName());
			CountryInfo newCountry = new CountryInfo();
			newCountry.setId(4);
			newCountry.setName("CountryD");
			newCountry.setCapital("CapitalD");
			newCountry.setPopulation(4000000);
			newCountry.setCurrency("CURD");

			CreateCountryResponse countryResponse = client.createCountryResponse(newCountry);
			LOGGER.info("countryResponse: " + countryResponse.getServiceStatus().getMessage());
		};
	}*/

}
