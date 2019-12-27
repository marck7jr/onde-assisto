using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OndeAssisto.Common.Models
{
    public abstract class Entidade : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private DateTime dataCriacao;
        private DateTime dataAtualizacao;

        public int Id { get => id; set => Set(ref id, value); }
        public DateTime DataCriacao { get => dataCriacao; set => Set(ref dataCriacao, value); }
        public DateTime DataAtualizacao { get => dataAtualizacao; set => Set(ref dataAtualizacao, value); }

        public virtual bool Set<T>(ref T campo, T valor, [CallerMemberName] string propriedade = null)
        {
            if (EqualityComparer<T>.Default.Equals(campo, valor))
            {
                return false;
            }
            else
            {
                campo = valor;
                OnNotifyPropertyChanged(propriedade);
                return true;
            }
        }

        private void OnNotifyPropertyChanged(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }
    }
}
