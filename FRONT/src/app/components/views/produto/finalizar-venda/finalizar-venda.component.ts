import { FinalizarVendaService } from './../../../../services/finalizar-venda.service';
import { FormapagamentoService } from './../../../../services/formapagamento.service';
import { Component, OnInit } from '@angular/core';
import { ItemVenda } from 'src/app/models/item-venda';
import { ItemService } from 'src/app/services/item.service';
import { FormaPagamento } from 'src/app/models/formas-pagamento';
import { Venda } from 'src/app/models/Venda';
import { Router } from '@angular/router';

@Component({
  selector: 'app-finalizar-venda',
  templateUrl: './finalizar-venda.component.html',
  styleUrls: ['./finalizar-venda.component.css']
})
export class FinalizarVendaComponent implements OnInit {
    cliente: string = "";
    formaPagamentoSelecionada: FormaPagamento = {nome: '', tipoPagamento: ''};
    itens: ItemVenda[] = [];
    formaPagamento?: FormaPagamento[] = [];
    colunasExibidas: String[] = ["nome", "preco", "quantidade", "imagem"];
    valorTotal!: number;
    constructor(private itemService: ItemService, private formapagamentoService: FormapagamentoService, private finalizarVendaService: FinalizarVendaService, private router: Router) {}


  ngOnInit(): void {
    let carrinhoId = localStorage.getItem("carrinhoId")! || "";
    this.itemService.getByCartId(carrinhoId).subscribe((itens) => {
        this.itens = itens;
        this.valorTotal = this.itens.reduce((total, item) => {
            return total + item.preco * item.quantidade;
        }, 0);
    });
    this.formapagamentoService.list().subscribe((data) => {
        this.formaPagamento = data;
    })
  }
    concluirVenda(){
        const objVenda: Venda = {
            itens: this.itens,
            cliente: this.cliente,
            formaPagamentoId: this.formaPagamentoSelecionada.formaPagamentoId!,
            formaPagamento: null//this.formaPagamentoSelecionada,

        }
        let itens = [];
        for (let index = 0; index < objVenda.itens.length; index++) {
            const {itemVendaId, produto, ...newObj} = objVenda.itens[index];
            itens.push(newObj);
        }

        objVenda.itens = itens;
        this.finalizarVendaService.create(objVenda).subscribe(()=> {
            this.router.navigate(["produto/listar"])
            localStorage.removeItem("carrinhoId");
        })
    }
}
