
package com.mapapi.wsdl;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Clase Java para anonymous complex type.
 * 
 * <p>El siguiente fragmento de esquema especifica el contenido que se espera que haya en esta clase.
 * 
 * <pre>{@code
 * <complexType>
 *   <complexContent>
 *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       <sequence>
 *         <element name="serviceStatus" type="{http://spring.io/guides/gs-producing-web-service}serviceStatus"/>
 *         <element name="countryInfo" type="{http://spring.io/guides/gs-producing-web-service}countryInfo"/>
 *       </sequence>
 *     </restriction>
 *   </complexContent>
 * </complexType>
 * }</pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "serviceStatus",
    "countryInfo"
})
@XmlRootElement(name = "createCountryResponse")
public class CreateCountryResponse {

    @XmlElement(required = true)
    protected ServiceStatus serviceStatus;
    @XmlElement(required = true)
    protected CountryInfo countryInfo;

    /**
     * Obtiene el valor de la propiedad serviceStatus.
     * 
     * @return
     *     possible object is
     *     {@link ServiceStatus }
     *     
     */
    public ServiceStatus getServiceStatus() {
        return serviceStatus;
    }

    /**
     * Define el valor de la propiedad serviceStatus.
     * 
     * @param value
     *     allowed object is
     *     {@link ServiceStatus }
     *     
     */
    public void setServiceStatus(ServiceStatus value) {
        this.serviceStatus = value;
    }

    /**
     * Obtiene el valor de la propiedad countryInfo.
     * 
     * @return
     *     possible object is
     *     {@link CountryInfo }
     *     
     */
    public CountryInfo getCountryInfo() {
        return countryInfo;
    }

    /**
     * Define el valor de la propiedad countryInfo.
     * 
     * @param value
     *     allowed object is
     *     {@link CountryInfo }
     *     
     */
    public void setCountryInfo(CountryInfo value) {
        this.countryInfo = value;
    }

}
