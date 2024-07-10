/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { createEmptyFromDiscriminatorValue, createEventFromDiscriminatorValue, createStatusFromDiscriminatorValue, serializeEvent, serializeUpdateEventRequest, type Empty, type Event, type Status, type UpdateEventRequest } from '../../../models/index.js';
// @ts-ignore
import { AttendeesRequestBuilderRequestsMetadata, type AttendeesRequestBuilder } from './attendees/index.js';
// @ts-ignore
import { AttendeesBatchAddRequestBuilderRequestsMetadata, type AttendeesBatchAddRequestBuilder } from './attendeesBatchAdd/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type KeysToExcludeForNavigationMetadata, type NavigationMetadata, type Parsable, type ParsableFactory, type RequestConfiguration, type RequestInformation, type RequestsMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1/events/{id}
 */
export interface EventsItemRequestBuilder extends BaseRequestBuilder<EventsItemRequestBuilder> {
    /**
     * The attendees property
     */
    get attendees(): AttendeesRequestBuilder;
    /**
     * The attendeesBatchAdd property
     */
    get attendeesBatchAdd(): AttendeesBatchAddRequestBuilder;
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<Empty>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     delete(requestConfiguration?: RequestConfiguration<object> | undefined) : Promise<Empty | undefined>;
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<Event>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     get(requestConfiguration?: RequestConfiguration<object> | undefined) : Promise<Event | undefined>;
    /**
     * @param body The request body
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<Event>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     patch(body: UpdateEventRequest, requestConfiguration?: RequestConfiguration<object> | undefined) : Promise<Event | undefined>;
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toDeleteRequestInformation(requestConfiguration?: RequestConfiguration<object> | undefined) : RequestInformation;
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toGetRequestInformation(requestConfiguration?: RequestConfiguration<object> | undefined) : RequestInformation;
    /**
     * @param body The request body
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toPatchRequestInformation(body: UpdateEventRequest, requestConfiguration?: RequestConfiguration<object> | undefined) : RequestInformation;
}
/**
 * Uri template for the request builder.
 */
export const EventsItemRequestBuilderUriTemplate = "{+baseurl}/v1/events/{id}";
/**
 * Metadata for all the navigation properties in the request builder.
 */
export const EventsItemRequestBuilderNavigationMetadata: Record<Exclude<keyof EventsItemRequestBuilder, KeysToExcludeForNavigationMetadata>, NavigationMetadata> = {
    attendees: {
        requestsMetadata: AttendeesRequestBuilderRequestsMetadata,
    },
    attendeesBatchAdd: {
        requestsMetadata: AttendeesBatchAddRequestBuilderRequestsMetadata,
    },
};
/**
 * Metadata for all the requests in the request builder.
 */
export const EventsItemRequestBuilderRequestsMetadata: RequestsMetadata = {
    delete: {
        uriTemplate: EventsItemRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createEmptyFromDiscriminatorValue,
    },
    get: {
        uriTemplate: EventsItemRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createEventFromDiscriminatorValue,
    },
    patch: {
        uriTemplate: EventsItemRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createEventFromDiscriminatorValue,
        requestBodyContentType: "application/json",
        requestBodySerializer: serializeUpdateEventRequest,
        requestInformationContentSetMethod: "setContentFromParsable",
    },
};
/* tslint:enable */
/* eslint-enable */
