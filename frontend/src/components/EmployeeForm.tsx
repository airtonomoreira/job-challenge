import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Employee } from "@/types/employee";

interface EmployeeFormProps {
  employee?: Employee;
  onSave: (employee: Employee) => void;
  onCancel: () => void;
}

export default function EmployeeForm({
  employee,
  onSave,
  onCancel,
}: EmployeeFormProps) {
  const [formData, setFormData] = useState<Employee>(
    employee || {
      id: "",
      userId: "",
      email: "",
      firstName: "",
      lastName: "",
      documentNumber: "",
      phoneNumbers: [""],
      managerId: null,
      managerName: null,
      dateOfBirth: "",
      role: "",
      hierarchicalLevel: 0,
      createdAt: null,
      updatedAt: "",
      password: "",
    }
  );

  const [managerSearch, setManagerSearch] = useState("");

  const fetchManagerId = async (name: string) => {
    try {
      const response = await fetch(
        `http://localhost:5179/api/Employees/byname/${name}`
      );
      if (!response.ok) {
        throw new Error("Gestor não encontrado");
      }
      const manager = await response.json();
      setFormData((prev) => ({
        ...prev,
        managerId: manager.id,
        managerName: manager.name,
      }));
    } catch (error) {
      console.error("Erro ao buscar gestor:", error);
      setFormData((prev) => ({ ...prev, managerId: null, managerName: null }));
    }
  };

  useEffect(() => {
    if (managerSearch) {
      const debounceTimer = setTimeout(() => {
        fetchManagerId(managerSearch);
      }, 500);
      return () => clearTimeout(debounceTimer);
    }
  }, [managerSearch]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4 mb-8">
      <div>
        <Label>Nome</Label>
        <Input
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Sobrenome</Label>
        <Input
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Email</Label>
        <Input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Número do Documento</Label>
        <Input
          name="documentNumber"
          value={formData.documentNumber}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Telefones</Label>
        {formData.phoneNumbers.map((phone, index) => (
          <Input
            key={index}
            value={phone}
            onChange={(e) => {
              const newPhones = [...formData.phoneNumbers];
              newPhones[index] = e.target.value;
              setFormData((prev) => ({ ...prev, phoneNumbers: newPhones }));
            }}
            required
          />
        ))}
        <Button
          type="button"
          onClick={() =>
            setFormData((prev) => ({
              ...prev,
              phoneNumbers: [...prev.phoneNumbers, ""],
            }))
          }
        >
          Adicionar Telefone
        </Button>
      </div>
      <div>
        <Label>Nome do Gestor</Label>
        <Input
          name="managerName"
          value={managerSearch}
          onChange={(e) => setManagerSearch(e.target.value)}
          placeholder="Digite o nome do gestor"
        />
        {formData.managerId && (
          <p className="text-sm text-gray-500">
            Gestor selecionado: {formData.managerName} (ID: {formData.managerId}
            )
          </p>
        )}
      </div>
      <div>
        <Label>Data de Nascimento</Label>
        <Input
          type="date"
          name="dateOfBirth"
          value={formData.dateOfBirth}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Cargo</Label>
        <Input
          name="role"
          value={formData.role}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Nível Hierárquico</Label>
        <Input
          type="number"
          name="hierarchicalLevel"
          value={formData.hierarchicalLevel}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <Label>Senha</Label>
        <Input
          type="password"
          name="password"
          value={formData.password || ""}
          onChange={handleChange}
          required={!employee}
        />
      </div>
      <div className="flex gap-2">
        <Button type="submit">Salvar</Button>
        <Button type="button" variant="outline" onClick={onCancel}>
          Cancelar
        </Button>
      </div>
    </form>
  );
}
