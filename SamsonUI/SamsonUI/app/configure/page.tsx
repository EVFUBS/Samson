"use client"

import { Input } from "@nextui-org/input";
import {Dropdown, DropdownTrigger, DropdownMenu, DropdownItem, Button, RadioGroup, Radio} from "@nextui-org/react";
import { useMemo, useState } from "react";
import Form from "@/components/form";
import ListenSettings from "./components/ListenSettings";
import ServerSettings from "./components/ServerSettings";

export default function Home() {
	const [selectedKeys, setSelectedKeys] = useState(new Set(["Code"]));

	const selectedValue = useMemo(
		() => Array.from(selectedKeys).join(", ").replaceAll("_", " "),
		[selectedKeys]
	  );

	return (
		<section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10 w-full">
            <p>Configure</p>
			<div className="flex flex-col w-full items-center space-y-32">
				<ListenSettings/>
				<ServerSettings/>

				<Form>
					<label>Register Spotify Integration (For Development)</label>
					<Input type="text" label="Spotify Client Id" placeholder="Enter spotify client id"></Input>
					<Input type="password" label="Spotify Client Secret" placeholder="Enter spotify client secret"></Input>
					<div className="flex flex-row justify-between items-center w-full">
						<p>Response Type</p>
						<Dropdown>
							<DropdownTrigger>
								<Button 
								variant="bordered" 
								className="capitalize"
								>
								{selectedValue}
								</Button>
							</DropdownTrigger>
							<DropdownMenu 
								aria-label="Single selection example"
								variant="flat"
								disallowEmptySelection
								selectionMode="single"
								selectedKeys={selectedKeys}
								onSelectionChange={setSelectedKeys}
							>
								<DropdownItem key="Code">Code</DropdownItem>
								<DropdownItem key="Not">Not</DropdownItem>
								<DropdownItem key="Sure">Sure</DropdownItem>
								<DropdownItem key="Other options">Other options</DropdownItem>
								<DropdownItem key="Yet">Yet</DropdownItem>
							</DropdownMenu>
						</Dropdown>
					</div>
					<Input type="text" label="RedirectUri" placeholder="http://localhost:7149/Spotify/callback">http://localhost:7149/Spotify/callback</Input>
					<Input type="text" label="Scopes" placeholder="user-read-playback-state user-modify-playback-state user-read-currently-playing">user-read-playback-state user-modify-playback-state user-read-currently-playing</Input>
				</Form>
			</div>
		</section>
	);
}