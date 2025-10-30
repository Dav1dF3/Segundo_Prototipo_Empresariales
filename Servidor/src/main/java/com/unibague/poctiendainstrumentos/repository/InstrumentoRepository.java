package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.model.Instrumento;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface InstrumentoRepository extends JpaRepository<Instrumento, Long> {
}
