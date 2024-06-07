"use client";

import * as React from "react";
import { NextUIProvider } from "@nextui-org/system";
import { useRouter } from 'next/navigation'
import { ThemeProvider as NextThemesProvider } from "next-themes";
import { ThemeProviderProps } from "next-themes/dist/types";
import { createContext, useReducer, useState } from "react";
import { SamsonTokenContext } from "@/config/globals";

export interface ProvidersProps {
	children: React.ReactNode;
	themeProps?: ThemeProviderProps;
}

export function Providers({ children, themeProps }: ProvidersProps) {
  const router = useRouter();

	return (
		<SamsonTokenProvider>
			<NextUIProvider navigate={router.push}>
				<NextThemesProvider {...themeProps}>{children}</NextThemesProvider>
			</NextUIProvider>
		</SamsonTokenProvider>
	);
}

interface SamsonTokenProviderProps {
	children: React.ReactNode
}

const SamsonTokenProvider: React.FC<SamsonTokenProviderProps> = ({ children }) => {
    const [token, setToken] = useState<string>("");
  
    return (
		<SamsonTokenContext.Provider value={{token, setToken}}>
			{children}
		</SamsonTokenContext.Provider>
    );
  };