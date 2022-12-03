import "../delete-page/events-delete.css";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import { Button, Header, Input } from "semantic-ui-react";
import React, { useState, useEffect } from "react";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import { ApiResponse, EventGetDto } from "../../../constants/types";
import toast from "react-hot-toast";

export const EventDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [event, setEvents] = useState<EventGetDto>();

  useEffect(() => {
    const fetchEvents = async () => {
      const response = await axios.get<ApiResponse<EventGetDto>>(
        `/api/events/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setEvents(response.data.data);
    };

    fetchEvents();
  });

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<EventGetDto>>(
      `/api/events/${id}`
    );

    if (response.data.hasErrors) {
      toast.error("Error Occured, please try again", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.events.listing);
      toast.success("Event successfully deleted", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };

  return (
    <>
      {event && (
        <Formik initialValues={event} onSubmit={onSubmit}>
          <Form>
            <div className="events-delete-container">
              <Header>Delete Event</Header>
            </div>
            <div className="events-delete-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="events-delete-container">
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-delete-container">
              <label htmlFor="eventDetails">Event Details</label>
            </div>
            <div className="events-delete-container">
              <Field id="eventDetails" name="eventDetails">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-delete-container">
              <label htmlFor="startDate">Start Date</label>
            </div>
            <div className="events-delete-container">
              <Field id="startDate" name="startDate">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-delete-container">
              <label htmlFor="endDate">End Date</label>
            </div>
            <div className="events-delete-container">
              <Field id="endDate" name="endDate">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-delete-container">
              <label htmlFor="calendar.group.name">Group Calendar</label>
            </div>
            <div className="events-delete-container">
              <Field id="calendar.group.name" name="calendar.group.name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="events-delete-container">
              <Button
                negative
                icon="trash"
                content="Delete"
                labelPosition="left"
                type="submit"
              />
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
