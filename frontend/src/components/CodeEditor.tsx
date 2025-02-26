"use client";
import { Editor } from "@monaco-editor/react";

export function CodeEditor({ existingCode }: any) {
  function handleEditorChange(value: string | undefined) {
    console.log(value);
  }
  return (
    <Editor
      height="90vh"
      defaultLanguage="javascript"
      defaultValue={existingCode}
      onChange={handleEditorChange}
    />
  );
}
