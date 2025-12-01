package com.ajm.spring.soap.endpoint;


import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;
import com.ajm.soap.gen.Country;
import com.ajm.soap.gen.CreateCountryRequest;
import com.ajm.soap.gen.CreateCountryResponse;
import com.ajm.soap.gen.DeleteCountryRequest;
import com.ajm.soap.gen.DeleteCountryResponse;
import com.ajm.soap.gen.GetAllCountriesRequest;
import com.ajm.soap.gen.GetAllCountriesResponse;
import com.ajm.soap.gen.GetCountryRequest;
import com.ajm.soap.gen.GetCountryResponse;
import com.ajm.soap.gen.ServiceStatus;
import com.ajm.soap.gen.UpdateCountryRequest;
import com.ajm.soap.gen.UpdateCountryResponse;
import com.ajm.spring.soap.converter.CountryConverter;
import com.ajm.spring.soap.model.CountryModel;
import com.ajm.spring.soap.repository.CountryRepository;

import jakarta.validation.Valid;
import jakarta.xml.bind.ValidationException;

@Endpoint
public class CountryEndpoint {
	private static final String NAMESPACE_URI = "http://com.ajm.soap.gen/gs-producing-web-service";

    @Autowired
    private CountryRepository countryRepository;

    @Autowired
    private CountryConverter countryConverter;

	@PayloadRoot(namespace = NAMESPACE_URI, localPart = "getCountryRequest")
    @ResponsePayload
    public GetCountryResponse getCountry(@RequestPayload GetCountryRequest request) {
    	GetCountryResponse response = new GetCountryResponse();
    	CountryModel countryModel = countryRepository.findByName(request.getName());
    	ServiceStatus serviceStatus = new ServiceStatus();
    	if(countryModel!=null) {
    		response.setCountry(countryConverter.converCountryModelToCountry(countryModel));
    		serviceStatus.setStatusCode("OK");
            serviceStatus.setMessage("Country GOTTEN succesfully");
            response.setStatus(serviceStatus);

    	}else {
    		response.setCountry(new Country());
    		serviceStatus.setStatusCode("ERROR");
            serviceStatus.setMessage("Country DOESN'T exists");
            response.setStatus(serviceStatus);
    	}
        
        return response;
    }
	
	@PayloadRoot(namespace = NAMESPACE_URI, localPart = "getAllCountriesRequest")
    @ResponsePayload
    public GetAllCountriesResponse getCountries(@RequestPayload GetAllCountriesRequest request) {
		GetAllCountriesResponse response = new GetAllCountriesResponse();
    	List<CountryModel> countriesModel = countryRepository.findAll();
    	
    	List<Country> countries = countryConverter.converCountriesModelToCountries(countriesModel);
        response.getCountry().addAll(countries);
        return response;
    }
    
    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "createCountryRequest")
    @ResponsePayload
    public CreateCountryResponse createCountry(@Valid @RequestPayload CreateCountryRequest request) throws ValidationException {
    	if(request.getCountry().getName().length()<3) {
	    	String reason = "Invalid Country name provided; The Country name must be greather then 3";
	        String details = "The Country name must be greather then 3, but received: " + request.getCountry().getName();
	        throw new ValidationException(reason, details);
    	}
    	if(request.getCountry().getPopulation()<0) {
	    	String reason = "Invalid Country population provided;The Country population must be greather then 0";
	        String details = "The Country population must be greather then 3, but received: " + request.getCountry().getName();
	        throw new ValidationException(reason, details);
    	}
    	if(request.getCountry().getCapital().length()<3) {
	    	String reason = "Invalid Country capital provided; The Country capital must be greather then 3";
	        String details = "The Country capital must be greather then 3, but received: " + request.getCountry().getName();
	        throw new ValidationException(reason, details);
    	}
    	if(request.getCountry().getCurrency().length()<2) {
	    	String reason = "Invalid Country currency provided; The Country currency must be greather then 2";
	        String details = "The Country currency must be greather then 3, but received: " + request.getCountry().getName();
	        throw new ValidationException(reason, details);
    	}
    	CreateCountryResponse response = new CreateCountryResponse();
    	ServiceStatus serviceStatus = new ServiceStatus();
    	CountryModel countryModel = countryRepository.findByName(request.getCountry().getName());
    	if(countryModel==null) {
    		countryModel = countryRepository.save(countryConverter.converCountryToCountryModel(request.getCountry()));
            serviceStatus.setStatusCode("OK");
            serviceStatus.setMessage("Country created succesfully");
    	}else {
            serviceStatus.setStatusCode("ERROR");
            serviceStatus.setMessage("Country already exists");
    	}
        response.setStatus(serviceStatus);;
        return response;
    }
    
    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "updateCountryRequest")
    @ResponsePayload
    public UpdateCountryResponse updateCountry(@RequestPayload UpdateCountryRequest request) {
    	UpdateCountryResponse response = new UpdateCountryResponse();
    	ServiceStatus serviceStatus = new ServiceStatus();
    	CountryModel countryModel = countryRepository.findByName(request.getCountry().getName());
    	if(countryModel!=null) {
    		countryModel = countryConverter.updateCountryToCountryModel(request.getCountry(), countryModel);
    		countryModel = countryRepository.save(countryModel);
    		serviceStatus.setStatusCode("OK");
            serviceStatus.setMessage("Country updated succesfully");
    	}else {
    		serviceStatus.setStatusCode("ERROR");
            serviceStatus.setMessage("Country DOESN'T exists");
    	}
        response.setStatus(serviceStatus);;
        return response;
    }
    
    
    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "deleteCountryRequest")
    @ResponsePayload
    public DeleteCountryResponse deleteCountry(@RequestPayload DeleteCountryRequest request) {
    	DeleteCountryResponse response = new DeleteCountryResponse();
    	ServiceStatus serviceStatus = new ServiceStatus();
    	CountryModel countryModel = countryRepository.findByName(request.getName());
    	if(countryModel!=null) {
    		countryRepository.delete(countryModel);
    		serviceStatus.setStatusCode("OK");
            serviceStatus.setMessage("Country deleted succesfully");
    	}else {
    		serviceStatus.setStatusCode("ERROR");
            serviceStatus.setMessage("Country DOESN'T exists");
    	}
        
        
        response.setStatus(serviceStatus);;
        return response;
    }
}
