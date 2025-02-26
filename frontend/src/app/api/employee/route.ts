// app/api/employees/route.ts
import { NextResponse } from "next/server";
import { Employee } from "@/types/employee";

// Dados fictícios para simular um banco de dados
let employees: Employee[] = [
  {
    id: "1",
    userId: "user1", // Adicionado para consistência
    firstName: "João",
    lastName: "Silva",
    email: "joao@example.com",
    documentNumber: "123456789",
    role: "Desenvolvedor",
    hierarchicalLevel: 1,
    phoneNumbers: ["11987654321"], // Corrigido de 'phones' para 'phoneNumbers'
    managerId: "2", // Sugestão: ID do gestor
    managerName: "Maria",
    dateOfBirth: "1990-05-15", // Adicionado para validação de idade
    createdAt: "2025-02-26",
    updatedAt: "2025-02-26",
    password: "senha123",
  },
];

// GET /api/employees
export async function GET() {
  return NextResponse.json(employees);
}

// POST /api/employees
export async function POST(request: Request) {
  const newEmployee: Employee = await request.json();
  newEmployee.id = (employees.length + 1).toString(); // Simula a geração de um ID
  employees.push(newEmployee);
  return NextResponse.json(newEmployee, { status: 201 });
}

// PUT /api/employees/:id
export async function PUT(
  request: Request,
  { params }: { params: { id: string } }
) {
  const id = parseInt(params.id);
  const updatedEmployee: Employee = await request.json();
  employees = employees.map((emp) =>
    emp.id === id.toString() ? { ...emp, ...updatedEmployee } : emp
  );
  return NextResponse.json(employees.find((emp) => emp.id === id.toString()));
}

// DELETE /api/employees/:id
export async function DELETE(
  request: Request,
  { params }: { params: { id: string } }
) {
  const id = parseInt(params.id);
  employees = employees.filter((emp) => emp.id !== id.toString());
  return NextResponse.json(
    { message: "Funcionário deletado com sucesso" },
    { status: 200 }
  );
}
