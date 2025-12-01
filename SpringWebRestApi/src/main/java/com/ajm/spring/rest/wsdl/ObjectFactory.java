
package com.ajm.spring.rest.wsdl;

import jakarta.xml.bind.annotation.XmlRegistry;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.ajm.spring.rest.wsdl package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {


    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.ajm.spring.rest.wsdl
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link GetCountryRequest }
     * 
     * @return
     *     the new instance of {@link GetCountryRequest }
     */
    public GetCountryRequest createGetCountryRequest() {
        return new GetCountryRequest();
    }

    /**
     * Create an instance of {@link GetCountryResponse }
     * 
     * @return
     *     the new instance of {@link GetCountryResponse }
     */
    public GetCountryResponse createGetCountryResponse() {
        return new GetCountryResponse();
    }

    /**
     * Create an instance of {@link Country }
     * 
     * @return
     *     the new instance of {@link Country }
     */
    public Country createCountry() {
        return new Country();
    }

    /**
     * Create an instance of {@link ServiceStatus }
     * 
     * @return
     *     the new instance of {@link ServiceStatus }
     */
    public ServiceStatus createServiceStatus() {
        return new ServiceStatus();
    }

    /**
     * Create an instance of {@link GetAllCountriesRequest }
     * 
     * @return
     *     the new instance of {@link GetAllCountriesRequest }
     */
    public GetAllCountriesRequest createGetAllCountriesRequest() {
        return new GetAllCountriesRequest();
    }

    /**
     * Create an instance of {@link GetAllCountriesResponse }
     * 
     * @return
     *     the new instance of {@link GetAllCountriesResponse }
     */
    public GetAllCountriesResponse createGetAllCountriesResponse() {
        return new GetAllCountriesResponse();
    }

    /**
     * Create an instance of {@link CreateCountryRequest }
     * 
     * @return
     *     the new instance of {@link CreateCountryRequest }
     */
    public CreateCountryRequest createCreateCountryRequest() {
        return new CreateCountryRequest();
    }

    /**
     * Create an instance of {@link CreateCountryResponse }
     * 
     * @return
     *     the new instance of {@link CreateCountryResponse }
     */
    public CreateCountryResponse createCreateCountryResponse() {
        return new CreateCountryResponse();
    }

    /**
     * Create an instance of {@link UpdateCountryRequest }
     * 
     * @return
     *     the new instance of {@link UpdateCountryRequest }
     */
    public UpdateCountryRequest createUpdateCountryRequest() {
        return new UpdateCountryRequest();
    }

    /**
     * Create an instance of {@link UpdateCountryResponse }
     * 
     * @return
     *     the new instance of {@link UpdateCountryResponse }
     */
    public UpdateCountryResponse createUpdateCountryResponse() {
        return new UpdateCountryResponse();
    }

    /**
     * Create an instance of {@link DeleteCountryRequest }
     * 
     * @return
     *     the new instance of {@link DeleteCountryRequest }
     */
    public DeleteCountryRequest createDeleteCountryRequest() {
        return new DeleteCountryRequest();
    }

    /**
     * Create an instance of {@link DeleteCountryResponse }
     * 
     * @return
     *     the new instance of {@link DeleteCountryResponse }
     */
    public DeleteCountryResponse createDeleteCountryResponse() {
        return new DeleteCountryResponse();
    }

}
