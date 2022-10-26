import axios from "axios";
import React, { useEffect, useState } from "react";
import { Segment } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, MealTypeGetDto } from "../../../constants/types";

export const MealTypeListingPage = () => {
    const [mealTypes, setMealTypes] = useState<MealTypeGetDto[]>();
    const fetchMealTypes = async() => {
        const response = await axios.get<ApiResponse<MealTypeGetDto[]>>(
            `${BaseUrl}/api/meal-types`
            );
        if(response.data.hasErrors){
            response.data.errors.forEach((err) => {
                console.log(err.message);
            });
        } else {
            setMealTypes(response.data.data);
        }
    };

    useEffect(() => {
        fetchMealTypes();
    }, []);

    return (
        <>
            <div>
                {mealTypes ? ( 
                mealTypes.map(mealType => {
                    return (
                        <Segment>
                            <div>Id: {mealType.id}</div>
                            <div>Name: {mealType.name}</div>
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