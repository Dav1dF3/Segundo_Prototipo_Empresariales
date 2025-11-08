package com.unibague.poctiendainstrumentos.model;

import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import java.io.Serializable;
import java.util.Objects;

@AllArgsConstructor
@NoArgsConstructor
public class FundaId implements Serializable {
    private long codigo;
    private long codigo_guitarra;


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof FundaId)) return false;
        FundaId that = (FundaId) o;
        return codigo == that.codigo && codigo_guitarra == that.codigo_guitarra;
    }

    @Override
    public int hashCode() {
         return Objects.hash(codigo, codigo_guitarra);
        }
    }
