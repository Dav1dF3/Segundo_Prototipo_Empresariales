package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.model.Funda;
import com.unibague.poctiendainstrumentos.model.FundaId;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface FundaRepository extends JpaRepository<Funda, FundaId> {
}
