import { Button } from "@/components/ui/button";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Employee } from "@/types/employee";
import { useRouter } from "next/navigation";

interface EmployeeListProps {
  employees: Employee[];
  onEdit: (employee: Employee) => void;
  onDelete: (id: string) => void;
}

export default function EmployeeList({
  employees,
  onEdit,
  onDelete,
}: EmployeeListProps) {
  const router = useRouter();

  const handleEdit = (employee: Employee) => {
    router.push(`/employee/${employee.id}`); 
  };

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Nome</TableHead>
          <TableHead>Email</TableHead>
          <TableHead>Documento</TableHead>
          <TableHead>Telefones</TableHead>
          <TableHead>Gestor</TableHead>
          <TableHead>Ações</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {employees.map((employee) => (
          <TableRow key={employee.id}>
            <TableCell>
              {employee.firstName} {employee.lastName}
            </TableCell>
            <TableCell>{employee.email}</TableCell>
            <TableCell>{employee.documentNumber}</TableCell>
            <TableCell>{employee.phoneNumbers.join(", ")}</TableCell>
            <TableCell>{employee.managerName || "N/A"}</TableCell>
            <TableCell>
              <Button onClick={() => handleEdit(employee)}>Editar</Button>
              <Button
                variant="destructive"
                onClick={() => onDelete(employee.id)}
              >
                Excluir
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
