package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.model.Guitarra;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface GuitarraRepository extends JpaRepository<Guitarra, Long> {
}
