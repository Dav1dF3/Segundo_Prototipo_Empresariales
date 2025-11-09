package com.unibague.poctiendainstrumentos.repository;

import com.unibague.poctiendainstrumentos.dto.FiltroInstrumentoDTO;
import com.unibague.poctiendainstrumentos.model.Instrumento;
import com.unibague.poctiendainstrumentos.model.enums.TipoGuitarra;
import java.util.List;

public interface InstrumentoRepositoryCustom {

    List<Instrumento> filtrarInstrumentos(FiltroInstrumentoDTO filtro);

}
