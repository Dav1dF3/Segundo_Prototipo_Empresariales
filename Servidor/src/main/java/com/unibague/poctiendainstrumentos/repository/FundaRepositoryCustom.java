package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.dto.FiltroFundaDTO;
import com.unibague.poctiendainstrumentos.model.Funda;

import java.util.List;

public interface FundaRepositoryCustom {

    List<Funda> filtrarFundas(FiltroFundaDTO filtro);
}
