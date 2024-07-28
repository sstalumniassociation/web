/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { AdmissionsRequestBuilderNavigationMetadata, type AdmissionsRequestBuilder } from './admissions/index.js';
// @ts-ignore
import { ArticlesRequestBuilderRequestsMetadata, type ArticlesRequestBuilder } from './articles/index.js';
// @ts-ignore
import { AttendeesRequestBuilderNavigationMetadata, type AttendeesRequestBuilder } from './attendees/index.js';
// @ts-ignore
import { AuthRequestBuilderNavigationMetadata, type AuthRequestBuilder } from './auth/index.js';
// @ts-ignore
import { EventsRequestBuilderNavigationMetadata, EventsRequestBuilderRequestsMetadata, type EventsRequestBuilder } from './events/index.js';
// @ts-ignore
import { type UsersRequestBuilder, UsersRequestBuilderNavigationMetadata, UsersRequestBuilderRequestsMetadata } from './users/index.js';
// @ts-ignore
import { type UsersBatchCreateRequestBuilder, UsersBatchCreateRequestBuilderRequestsMetadata } from './usersBatchCreate/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type KeysToExcludeForNavigationMetadata, type NavigationMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1
 */
export interface V1RequestBuilder extends BaseRequestBuilder<V1RequestBuilder> {
    /**
     * The admissions property
     */
    get admissions(): AdmissionsRequestBuilder;
    /**
     * The articles property
     */
    get articles(): ArticlesRequestBuilder;
    /**
     * The attendees property
     */
    get attendees(): AttendeesRequestBuilder;
    /**
     * The auth property
     */
    get auth(): AuthRequestBuilder;
    /**
     * The events property
     */
    get events(): EventsRequestBuilder;
    /**
     * The users property
     */
    get users(): UsersRequestBuilder;
    /**
     * The usersBatchCreate property
     */
    get usersBatchCreate(): UsersBatchCreateRequestBuilder;
}
/**
 * Uri template for the request builder.
 */
export const V1RequestBuilderUriTemplate = "{+baseurl}/v1";
/**
 * Metadata for all the navigation properties in the request builder.
 */
export const V1RequestBuilderNavigationMetadata: Record<Exclude<keyof V1RequestBuilder, KeysToExcludeForNavigationMetadata>, NavigationMetadata> = {
    admissions: {
        navigationMetadata: AdmissionsRequestBuilderNavigationMetadata,
    },
    articles: {
        requestsMetadata: ArticlesRequestBuilderRequestsMetadata,
    },
    attendees: {
        navigationMetadata: AttendeesRequestBuilderNavigationMetadata,
    },
    auth: {
        navigationMetadata: AuthRequestBuilderNavigationMetadata,
    },
    events: {
        requestsMetadata: EventsRequestBuilderRequestsMetadata,
        navigationMetadata: EventsRequestBuilderNavigationMetadata,
    },
    users: {
        requestsMetadata: UsersRequestBuilderRequestsMetadata,
        navigationMetadata: UsersRequestBuilderNavigationMetadata,
    },
    usersBatchCreate: {
        requestsMetadata: UsersBatchCreateRequestBuilderRequestsMetadata,
    },
};
/* tslint:enable */
/* eslint-enable */
