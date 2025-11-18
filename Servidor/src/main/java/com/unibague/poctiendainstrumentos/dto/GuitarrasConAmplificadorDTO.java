package com.unibague.poctiendainstrumentos.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class GuitarrasConAmplificadorDTO {

    private String marcaGuitarra;
    private String materialCuerpoGuitarra;
    private List<AmplificadorDTO> amplificadores;

}
