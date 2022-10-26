import axios from "axios";
import React, { useEffect, useState } from "react";
import { Segment } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, IngredientGetDto } from "../../../constants/types";

export const IngredientListingPage = () => {
    const [ingredients, setIngredients] = useState<IngredientGetDto[]>();
    const fetchIngredients = async() => {
        const response = await axios.get<ApiResponse<IngredientGetDto[]>>(
            `${BaseUrl}/api/ingredients`
            );
        if(response.data.hasErrors){
            response.data.errors.forEach((err) => {
                console.log(err.message);
            });
        } else {
            setIngredients(response.data.data);
        }
    };

    useEffect(() => {
        fetchIngredients();
    }, []);

    return (
        <>
            <div>
                {ingredients ? ( 
                ingredients.map(ingredient => {
                    return (
                        <Segment>
                            <div>Id: {ingredient.id}</div>
                            <div>Name: {ingredient.name}</div>
                            <div>Image: {ingredient.image}</div>
                        </Segment>
                    );
                }) 
            ) : (
                <div>Loading</div>
            )}
        </div>
        </>
    );
};