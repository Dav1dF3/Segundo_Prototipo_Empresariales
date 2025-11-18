package com.unibague.poctiendainstrumentos.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonFormat;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class AmplificadorDTO {
    private int id;
    private String marca;
    private String modelo;
    private double potencia;

    @JsonProperty("tipo_tubo")
    private String tipoTubo;

    @JsonProperty("fecha_fabricacion")
    @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd")
    private LocalDate fechaFabricacion;
}
