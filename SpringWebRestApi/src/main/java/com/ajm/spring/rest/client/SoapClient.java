package com.ajm.spring.rest.client;

import org.springframework.ws.client.core.support.WebServiceGatewaySupport;
import org.springframework.ws.soap.client.core.SoapActionCallback;

import com.ajm.spring.rest.wsdl.Country;
import com.ajm.spring.rest.wsdl.CreateCountryRequest;
import com.ajm.spring.rest.wsdl.CreateCountryResponse;
import com.ajm.spring.rest.wsdl.DeleteCountryRequest;
import com.ajm.spring.rest.wsdl.DeleteCountryResponse;
import com.ajm.spring.rest.wsdl.GetAllCountriesRequest;
import com.ajm.spring.rest.wsdl.GetAllCountriesResponse;
import com.ajm.spring.rest.wsdl.GetCountryRequest;
import com.ajm.spring.rest.wsdl.GetCountryResponse;
import com.ajm.spring.rest.wsdl.UpdateCountryRequest;
import com.ajm.spring.rest.wsdl.UpdateCountryResponse;

public class SoapClient extends WebServiceGatewaySupport{
	
	public GetCountryResponse getCountryResponse(String name) {
        GetCountryRequest GetByIdRequest = new GetCountryRequest();
        GetByIdRequest.setName(name);
        SoapActionCallback soapActionCallback = new SoapActionCallback("http://com.ajm.soap.gen/gs-producing-web-service/getCountryRequest");
        GetCountryResponse response =(GetCountryResponse) getWebServiceTemplate().marshalSendAndReceive("http://spring-app:8080/ws/countries.wsdl",GetByIdRequest, soapActionCallback);
        return response;
    }

	public GetAllCountriesResponse getAllCountryResponse() {
        GetAllCountriesRequest GetByIdRequest = new GetAllCountriesRequest();
        SoapActionCallback soapActionCallback = new SoapActionCallback("http://com.ajm.soap.gen/gs-producing-web-service/getAllCountryRequest");
        GetAllCountriesResponse response =(GetAllCountriesResponse) getWebServiceTemplate().marshalSendAndReceive("http://spring-app:8080/ws/countries.wsdl",GetByIdRequest, soapActionCallback);
        return response;
    }
    public CreateCountryResponse createCountryResponse(Country countryInfo) {

        CreateCountryRequest request = new CreateCountryRequest();
        request.setCountry(countryInfo);
        SoapActionCallback soapActionCallback = new SoapActionCallback("http://com.ajm.soap.gen/gs-producing-web-service/CreateCountryRequest");
        CreateCountryResponse response = (CreateCountryResponse) getWebServiceTemplate()
                .marshalSendAndReceive("http://spring-app:8080/ws/countries.wsdl", request, soapActionCallback);

        return response;
    }
    
    public UpdateCountryResponse updateCountryResponse(Country countryInfo) {

        UpdateCountryRequest request = new UpdateCountryRequest();
        request.setCountry(countryInfo);
        SoapActionCallback soapActionCallback = new SoapActionCallback("http://com.ajm.soap.gen/gs-producing-web-service/UpdateCountryRequest");
        UpdateCountryResponse response = (UpdateCountryResponse) getWebServiceTemplate()
                .marshalSendAndReceive("http://spring-app:8080/ws/countries.wsdl", request, soapActionCallback);

        return response;
    }

	public DeleteCountryResponse deleteCountryResponse(Country countryInfo) {
		DeleteCountryRequest countryRequest = new DeleteCountryRequest();
		countryRequest.setName(countryInfo.getName());
		SoapActionCallback soapActionCallback = new SoapActionCallback("http://com.ajm.soap.gen/gs-producing-web-service/DeleteCountryRequest");
        DeleteCountryResponse response = (DeleteCountryResponse) getWebServiceTemplate()
                .marshalSendAndReceive("http://spring-app:8080/ws/countries.wsdl", countryRequest, soapActionCallback);
		return response;
	}
}
