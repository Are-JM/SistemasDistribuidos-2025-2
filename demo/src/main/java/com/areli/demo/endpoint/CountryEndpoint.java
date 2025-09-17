package com.areli.demo.endpoint;

import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;
import jakarta.validation.Valid;

import com.areli.demo.services.CountryServiceImpl;
import com.areli.demo.domain.Country;
import io.spring.guides.gs_producing_web_service.*;

@Endpoint
public class CountryEndpoint {
	private static final String NAMESPACE_URI = "http://spring.io/guides/gs-producing-web-service";

	@Autowired
	private CountryServiceImpl countryService;

	@PayloadRoot(namespace = NAMESPACE_URI, localPart = "createCountryRequest")
	@ResponsePayload
	public CreateCountryResponse createCountry(@RequestPayload @Valid CreateCountryRequest request){
		CreateCountryResponse response = new CreateCountryResponse();
		ServiceStatus serviceStatus = new ServiceStatus();

		Country country = new Country();
		CountryInfo countryInfo = new CountryInfo();
		BeanUtils.copyProperties(request.getCountryInfo(), country);
		Country savedCountry = countryService.addCountry(country);
		BeanUtils.copyProperties(savedCountry, countryInfo);
		serviceStatus.setStatus("SUCCESS");
		serviceStatus.setMessage("Country created successfully");
		response.setServiceStatus(serviceStatus);
		return response;
	} 

	@PayloadRoot(namespace = NAMESPACE_URI, localPart = "getCountryByIdRequest")
	@ResponsePayload
	public GetCountryResponse getCountry(@RequestPayload GetCountryByIdRequest request){
		GetCountryResponse response = new GetCountryResponse();
		CountryInfo countryInfo = new CountryInfo();
		BeanUtils.copyProperties(countryService.getCountryById(request.getId()),countryInfo);
		response.setCountryInfo(countryInfo);
		return response;
	}
}