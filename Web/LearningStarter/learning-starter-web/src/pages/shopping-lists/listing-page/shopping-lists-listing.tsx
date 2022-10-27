import axios from "axios";
import React, { useEffect, useState } from "react";
import { Segment } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, ShoppingListGetDto } from "../../../constants/types";

export const ShoppingListListingPage = () => {
    const [shoppingLists, setShoppingLists] = useState<ShoppingListGetDto[]>();
    const fetchShoppingLists = async() => {
        const response = await axios.get<ApiResponse<ShoppingListGetDto[]>>(
            `${BaseUrl}/api/shopping-lists`
            );
        if(response.data.hasErrors){
            response.data.errors.forEach((err) => {
                console.log(err.message);
            });
        } else {
            setShoppingLists(response.data.data);
        }
    };

    useEffect(() => {
        fetchShoppingLists();
    }, []);

    return (
        <>
            <div>
                {shoppingLists ? ( 
                shoppingLists.map(shoppingList => {
                    return (
                        <Segment>
                            <div>Id: {shoppingList.id}</div>
                            <div>Name: {shoppingList.name}</div>
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