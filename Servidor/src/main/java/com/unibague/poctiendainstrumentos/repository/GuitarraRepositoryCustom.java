package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.dto.FiltroInstrumentoDTO;
import com.unibague.poctiendainstrumentos.model.Guitarra;
import java.util.List;

public interface GuitarraRepositoryCustom {

    List<Guitarra> filtrarGuitarras(FiltroInstrumentoDTO filtro);

}
