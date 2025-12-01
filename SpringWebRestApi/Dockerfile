FROM eclipse-temurin:17-jdk-jammy AS build

WORKDIR /app

COPY . .
#COPY src ./src
#RUN chmod +x mvnw
RUN ./mvnw clean install -Dmaven.test.skip=true

FROM eclipse-temurin:17-jre-jammy AS final
WORKDIR /app

VOLUME [ "/tmp" ]

#ARG JAR_FILE=target/SpringWebSOAP-1-0.0.1-SNAPSHOT.jar

COPY --from=build /app/target/*.jar ./restapp.jar
EXPOSE 8080
ENTRYPOINT ["java", "-jar", "restapp.jar"]