import axios from "axios";
import { Field, Form, Formik } from "formik";
import React from "react";
import { Button, Input } from "semantic-ui-react";
import { useHistory } from "react-router-dom";
import {
  ApiResponse,
  EventCreateDto,
  EventGetDto,
} from "../../../constants/types";
import { BaseUrl } from "../../../constants/env-cars";
import { routes } from "../../../routes/config";

const initialValues: EventCreateDto = {
  calendarId: 0,
  name: "",
  eventDetails: "",
  startDate: new Date(),
  endDate: new Date(),
};

export const EventCreatePage = () => {
  const history = useHistory();
  const onSubmit = async (values: EventCreateDto) => {
    const response = await axios.post<ApiResponse<EventGetDto>>(
      `${BaseUrl}/api/events`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.events.listing);
    }
  };
  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Form>
          <div>
            <label htmlFor="name">Name</label>
          </div>
          <Field id="name" name="name">
            {({ field }) => <Input {...field} />}
          </Field>
          <div>
            <label htmlFor="eventDetails">Event Details</label>
          </div>
          <Field id="eventDetails" name="eventDetails">
            {({ field }) => <Input {...field} />}
          </Field>
          <div>
            <label htmlFor="calendarId">CalendarId</label>
          </div>
          <Field id="calendarId" name="calendarId">
            {({ field }) => <Input type="number" {...field} />}
          </Field>
          <div>
            <label htmlFor="createdDate">Created Date</label>
          </div>
          <Field id="createdDate" name="createdDate">
            {({ field }) => <Input type="number" {...field} />}
          </Field>

          <div>
            <Button type="submit">Create</Button>
          </div>
        </Form>
      </Formik>
    </>
  );
};
