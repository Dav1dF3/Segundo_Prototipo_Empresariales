package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.model.Teclado;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface TecladoRepository extends JpaRepository<Teclado, Long> {
}
