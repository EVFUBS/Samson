"use client"

import Form from "@/components/form";
import { Input } from "@nextui-org/input";
import {useState} from "react";
import {samsonServerClient} from "@/clients/Clients";
import Actions from "@/types/enums";

export default function Page() {
	const [actionsAnswer, setActionsAnswer] = useState<string>(". . .");
	const [actionCommand, setActionCommand] = useState<string>()
		
	const onSamsonActionClick = async () => {
		const response = await samsonServerClient.action(actionCommand);
		// @ts-ignore
		let enumKey = Object.keys(Actions)[Object.values(Actions).indexOf(response.action)];
		setActionsAnswer(enumKey);
	}

	return (
		<div className="flex flex-col justify-center items-center w-full gap-20">
			<div> Wake demo will go here</div>
			<div className="flex flex-col items-center w-full gap-5">
				<div>
					<h3>Actions Inference</h3>
				</div>
				<p>Answer: {actionsAnswer}</p>
				<Form onClick={onSamsonActionClick} buttonText="Send">
					<Input type="text" label="Enter command for samson to see the output action!" placeholder="Enter command" onChange={(e) => setActionCommand(e.target.value)}></Input>
				</Form>
			</div>
			<div>Named entity recognition demo will go here</div>
		</div>
	)
}