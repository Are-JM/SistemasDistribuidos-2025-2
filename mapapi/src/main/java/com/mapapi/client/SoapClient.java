package com.mapapi.client;

import java.util.logging.Logger;

import org.springframework.cache.annotation.Cacheable;
import org.springframework.ws.client.core.support.WebServiceGatewaySupport;
import org.springframework.ws.soap.client.core.SoapActionCallback;

import com.mapapi.wsdl.CountryInfo;
import com.mapapi.wsdl.CreateCountryResponse;
import com.mapapi.wsdl.CreateCountryRequest;
import com.mapapi.wsdl.GetCountryByIdRequest;
import com.mapapi.wsdl.GetCountryResponse;

public class SoapClient extends WebServiceGatewaySupport {
    private static final Logger LOGGER = Logger.getLogger(SoapClient.class.getName());

    /**@Cacheable(value = "countries", key = "#Id")+*/
    public GetCountryResponse getCountryResponse(long Id) {
        LOGGER.info("Country Name: getCountryResponse " );
        GetCountryByIdRequest GetByIdRequest = new GetCountryByIdRequest();
        GetByIdRequest.setId(Id);

        SoapActionCallback soapActionCallback = new SoapActionCallback("http://spring.io/guides/gs-producing-web-service/GetCountryByIdRequest");
        LOGGER.info("Country Name: " + GetByIdRequest.getId());
        GetCountryResponse response =(GetCountryResponse) getWebServiceTemplate().marshalSendAndReceive("http://localhost:8080/ws/countries.wsdl",GetByIdRequest, soapActionCallback);
        LOGGER.info("response: " + response.getCountryInfo().getName());
        return response;
    }

    public CreateCountryResponse createCountryResponse(CountryInfo countryInfo) {

        CreateCountryRequest request = new CreateCountryRequest();
        request.setCountryInfo(countryInfo);
        SoapActionCallback soapActionCallback = new SoapActionCallback("http://spring.io/guides/gs-producing-web-service/CreateCountryRequest");
        LOGGER.info("CreateCountryResponse: " + request.getCountryInfo().getName());
        LOGGER.info("SoapActionCallback: " + soapActionCallback.toString());
        CreateCountryResponse response = (CreateCountryResponse) getWebServiceTemplate()
                .marshalSendAndReceive("http://localhost:8080/ws/countries.wsdl", request, soapActionCallback);

        return response;
    }
}

