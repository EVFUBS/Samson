"use client"

import { Input } from "@nextui-org/input";
import {Dropdown, DropdownTrigger, DropdownMenu, DropdownItem, Button, RadioGroup, Radio} from "@nextui-org/react";
import { Key, useMemo, useState } from "react";
import Form from "@/components/form";
import ListenSettings from "./components/ListenSettings";
import ServerSettings from "./components/ServerSettings";
import SpotifySettings from "./components/SpotifySettings";

export default function Home() {
	return (
		<section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10 w-full">
            <p>Configure</p>
			<div className="flex flex-col w-full items-center space-y-32">
				<ListenSettings/>
				<ServerSettings/>
				<SpotifySettings/>
			</div>
		</section>
	);
}