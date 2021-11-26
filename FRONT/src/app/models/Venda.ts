import { FormaPagamento } from "./formas-pagamento";
import { ItemVenda } from "./item-venda";

export interface Venda {
    vendaId?: number;
    cliente: string;
    formaPagamentoId: number;
    formaPagamento: FormaPagamento | null;
    itens: ItemVenda[];
    criadoEm?: Date;
}