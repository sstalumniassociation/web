/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { AttendeesItemRequestBuilderRequestsMetadata, type AttendeesItemRequestBuilder } from './item/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type KeysToExcludeForNavigationMetadata, type NavigationMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1/attendees
 */
export interface AttendeesRequestBuilder extends BaseRequestBuilder<AttendeesRequestBuilder> {
    /**
     * Gets an item from the ApiSdk.v1.attendees.item collection
     * @param id Unique identifier of the item
     * @returns {AttendeesItemRequestBuilder}
     */
     byId(id: string) : AttendeesItemRequestBuilder;
}
/**
 * Uri template for the request builder.
 */
export const AttendeesRequestBuilderUriTemplate = "{+baseurl}/v1/attendees";
/**
 * Metadata for all the navigation properties in the request builder.
 */
export const AttendeesRequestBuilderNavigationMetadata: Record<Exclude<keyof AttendeesRequestBuilder, KeysToExcludeForNavigationMetadata>, NavigationMetadata> = {
    byId: {
        requestsMetadata: AttendeesItemRequestBuilderRequestsMetadata,
        pathParametersMappings: ["id"],
    },
};
/* tslint:enable */
/* eslint-enable */
