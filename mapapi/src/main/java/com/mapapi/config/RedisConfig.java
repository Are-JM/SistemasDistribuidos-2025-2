package com.mapapi.config;

import java.time.Duration;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.redis.cache.RedisCacheConfiguration;
import org.springframework.data.redis.cache.RedisCacheManager;
import org.springframework.data.redis.connection.RedisConnectionFactory;
import org.springframework.data.redis.serializer.Jackson2JsonRedisSerializer;
import org.springframework.data.redis.serializer.RedisSerializationContext;

import com.mapapi.dtos.CountryCreateDto;

@Configuration
public class RedisConfig {
    
    @Bean
    public RedisCacheManager cacheManager(RedisConnectionFactory redisConnectionFactory) {
        RedisCacheConfiguration redisCacheConfiguration = RedisCacheConfiguration.defaultCacheConfig()
        .entryTtl(Duration.ofMinutes(3))
        .disableCachingNullValues()
        .serializeValuesWith(RedisSerializationContext.SerializationPair
        .fromSerializer(new Jackson2JsonRedisSerializer<>(CountryCreateDto.class)));
        return RedisCacheManager.builder(redisConnectionFactory).cacheDefaults(redisCacheConfiguration)
        .build();
    }
}
